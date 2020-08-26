#include "ftd2xx.h"
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <memory.h>

#include <time.h>

unsigned long read4bytes(FILE *f)
{
    unsigned char buf[4];

    if (fread(buf, 4, 1, f) != 1) {
        fprintf(stderr, "Read error\n");
        exit(1);
    }
    return ((256LU * buf[3] + buf[2]) * 256LU + buf[1] ) * 256LU + buf[0]  ;
}

unsigned read2bytes(FILE *f)
{
    unsigned char buf[2];

    if (fread(buf, 2, 1, f) != 1) {
        fprintf(stderr, "Read error\n");
        exit(1);
    }
    return 256U * buf[1] + buf[0] ;
}

void main(int argc, char * argv[])
{
	char *filename = argv[1];
	FILE *f;
    int i, channels, bits;
	unsigned long len;
	unsigned char s[10];
	f = fopen(filename, "rb");

    if (f == NULL) {
        printf("Can not open %s\n", filename);
        return;
    }
    printf("finename = '%s'\n", filename);
    if (fread(s, 4, 1, f) != 1) {
        printf("Read error\n");
        fclose(f);
        return;
    }
    if (memcmp(s, "RIFF", 4) != 0) {
        printf("Not a RIFF format\n");
        fclose(f);
        return;
    }
    printf("[RIFF] (%lu bytes)\n", read4bytes(f));
    if (fread(s, 8, 1, f) != 1) {
        printf("Read error\n");
        fclose(f);
        return;
    }
    if (memcmp(s, "WAVEfmt ", 8) != 0) {
        printf("Not a WAVEfmt format\n");
        fclose(f);
        return;
    }
    len = read4bytes(f);
    printf("[WAVEfmt ] (%lu bytes)\n", len);
    if (len != 16) {
        printf("Length of WAVEfmt must be 16\n");
        return;
    }
    printf("  Data type = %u (1 = PCM)\n", read2bytes(f));
    channels = read2bytes(f);
    printf("  Number of channels = %u (1 = mono, 2 = stereo)\n", channels);
    printf("  Sampling rate = %luHz\n", read4bytes(f));
    printf("  Bytes per second = %lu\n", read4bytes(f));
	int BytesPerSample = read2bytes(f);
    printf("  Bytes per sample = %u\n", BytesPerSample);
    bits = read2bytes(f);
    printf("  Bits per sample = %u\n", bits);

    while (fread(s, 4, 1, f) == 1) 
	{
        len = read4bytes(f);
        s[4] = 0;
        printf("[%s] (%lu bytes)\n", s, len);
        if (memcmp(s, "data", 4) == 0) break;
        for (i = 0; i < (int)len; i++)
            printf("%02x ", fgetc(f));
        printf("\n");
    }
	//get memory for "len" bytes
	unsigned char * p;
	p = (unsigned char *)malloc(len);

	fread(p, len, 1,f);	//p has wave format data, 16bit

	fclose(f);

	FT_STATUS ftStatus;
	FT_HANDLE ftHandle;

	ftHandle = FT_W32_CreateFile("UM232H",GENERIC_READ|GENERIC_WRITE,0,0,
		OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL | FT_OPEN_BY_DESCRIPTION, 0);

	if (ftHandle == INVALID_HANDLE_VALUE)
	{
	// FT_Open failed
		printf("Can't open FTDI Device.\n");
		return;
	}

	ftStatus = FT_ResetDevice(ftHandle);
	Sleep(100);

	ftStatus = FT_Purge(ftHandle, FT_PURGE_RX | FT_PURGE_TX);
	Sleep(100);
	ftStatus = FT_SetTimeouts(ftHandle, 1000, 1000);
	Sleep(100);
	ftStatus = FT_SetLatencyTimer(ftHandle, 4); 
	Sleep(100);
	ftStatus = FT_SetBaudRate(ftHandle, 115200);
	Sleep(100);
	ftStatus = FT_SetUSBParameters(ftHandle, 0x10000, 0x10000);
	Sleep(100);

	unsigned char Mode;
	UCHAR Mask = 0xff;
	Mode = 0x00; //reset mode
	ftStatus = FT_SetBitMode(ftHandle, Mask, Mode);
	Sleep(100);

	int iSamples = 512;
	DWORD dwWaveBytes;
	dwWaveBytes = iSamples * BytesPerSample;	// L,R, 16bit/24bit
	DWORD dwBufferBytes;
	int headerbytes = 6;
	dwBufferBytes =  iSamples * (headerbytes + 3*24);	//24bit/channel * 24channels + header

	unsigned char * pSend;	//send buffer
	pSend = (unsigned char *)malloc(dwBufferBytes); 
	memset(pSend, 0, dwBufferBytes);

	unsigned long pCounter = 0;
	DWORD dwWritten;

	clock_t start,end;
	start = clock();
	unsigned char LH = 0x00;
	unsigned char LM = 0x00;
	unsigned char LL = 0x00;
	unsigned char RH = 0x00;
	unsigned char RM = 0x00;
	unsigned char RL = 0x00;

	while (pCounter < len - dwWaveBytes) {
		for (int j = 0; j < iSamples; j++) 
		{
			if (BytesPerSample == 4) {
				LL = 0x00;
				LM = *(p + pCounter + j*4 + 0); //read actual data
				LH = *(p + pCounter + j*4 + 1);
				RL = 0x00;
				RM = *(p + pCounter + j*4 + 2);
				RH = *(p + pCounter + j*4 + 3);			
			}
			if (BytesPerSample == 6) {
				LL = *(p + pCounter + j*6 + 0); //read actual data
				LM = *(p + pCounter + j*6 + 1); 
				LH = *(p + pCounter + j*6 + 2);
				RL = *(p + pCounter + j*6 + 3);
				RM = *(p + pCounter + j*6 + 4); 
				RH = *(p + pCounter + j*6 + 5);			
			}
			*(pSend + j*(headerbytes + 3*24) + 0) = 0x7f;	//write frame mark
			*(pSend + j*(headerbytes + 3*24) + 1) = 0xff;
			*(pSend + j*(headerbytes + 3*24) + 2) = 0xff;
			*(pSend + j*(headerbytes + 3*24) + 3) = 0x80;
			*(pSend + j*(headerbytes + 3*24) + 4) = 0x00;
			*(pSend + j*(headerbytes + 3*24) + 5) = 0x00;
			*(pSend + j*(headerbytes + 3*24) + headerbytes + 0) = LH;
			*(pSend + j*(headerbytes + 3*24) + headerbytes + 1) = LM;
			*(pSend + j*(headerbytes + 3*24) + headerbytes + 2) = LL;
			*(pSend + j*(headerbytes + 3*24) + headerbytes + 3) = RH;
			*(pSend + j*(headerbytes + 3*24) + headerbytes + 4) = RM;
			*(pSend + j*(headerbytes + 3*24) + headerbytes + 5) = RL;
		}

		//Syncronous
		if (FT_W32_WriteFile(ftHandle, pSend,  dwBufferBytes, &dwWritten, NULL)) { //modified wav
			if ( dwBufferBytes == dwWritten) {
				//OK
			} else {
				ftStatus = FT_W32_GetLastError(ftHandle);
				printf("FT_W32_WriteFile Length Error, Code = %d\n", ftStatus);
				FT_W32_PurgeComm(ftHandle, PURGE_TXABORT | PURGE_RXABORT | PURGE_TXCLEAR | PURGE_RXCLEAR);
				pCounter = len; //quit
			}
		}
		pCounter += dwWaveBytes;
	}

	end = clock();
	printf("dwWaveBytes = %d\n",dwWaveBytes);
	printf("%.2fSeconds, %d Bytes, %.2f KBytes/Sec \n",(double)(end-start)/CLOCKS_PER_SEC, len, (len/1000) / ((double)(end-start)/CLOCKS_PER_SEC));
	

	free(p);
	free(pSend);
	FT_W32_CloseHandle(ftHandle);

	return;

}