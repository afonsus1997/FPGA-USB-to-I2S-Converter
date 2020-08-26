#include <stdio.h>
#include <time.h>
#include "ftd2xx.h"

int main(int argc, char** argv) {

	FT_HANDLE handle;

	// check how many FTDI devices are attached to this PC
	unsigned long deviceCount = 0;
	if (FT_CreateDeviceInfoList(&deviceCount) != FT_OK) {
		printf("Unable to query devices. Exiting.\r\n");
		return 1;
	}

	// get a list of information about each FTDI device
	FT_DEVICE_LIST_INFO_NODE* deviceInfo = (FT_DEVICE_LIST_INFO_NODE*)malloc(sizeof(FT_DEVICE_LIST_INFO_NODE) * deviceCount);
	if (FT_GetDeviceInfoList(deviceInfo, &deviceCount) != FT_OK) {
		printf("Unable to get the list of info. Exiting.\r\n");
		return 1;
	}

	// print the list of information
	for (unsigned long i = 0; i < deviceCount; i++) {

		printf("Device = %d\r\n", i);
		printf("Flags = 0x%X\r\n", deviceInfo[i].Flags);
		printf("Type = 0x%X\r\n", deviceInfo[i].Type);
		printf("ID = 0x%X\r\n", deviceInfo[i].ID);
		printf("LocId = 0x%X\r\n", deviceInfo[i].LocId);
		printf("SN = %s\r\n", deviceInfo[i].SerialNumber);
		printf("Description = %s\r\n", deviceInfo[i].Description);
		printf("Handle = 0x%X\r\n", deviceInfo[i].ftHandle);
		printf("\r\n");

		// connect to the device with SN "FT3SSN2O"
		if (deviceInfo[i].LocId == 0x216) {

			if (FT_OpenEx((void*)deviceInfo[i].LocId, FT_OPEN_BY_LOCATION, &handle) == FT_OK &&
				FT_SetBitMode(handle, 0xFF, 0x1) == FT_OK &&
				FT_SetLatencyTimer(handle, 2) == FT_OK &&
				FT_SetUSBParameters(handle, 128, 128) == FT_OK &&
				FT_SetFlowControl(handle, FT_FLOW_RTS_CTS, 0, 0) == FT_OK &&
				FT_Purge(handle, FT_PURGE_RX | FT_PURGE_TX) == FT_OK &&
				FT_SetTimeouts(handle, 10000, 10000) == FT_OK) {

				// connected and configured successfully
				printf("Device connected!\r\n");
				// read 1GB of data from the FTDI/FPGA
				char txBuffer[8] = { 0xFF , 0xFF , 0xFF , 0xFF , 0xFF , 0xFF , 0xFF , 0xFF };
				unsigned long byteCount = 0;
				time_t startTime = clock();
				FT_STATUS error;
				for (int i = 0; i < 8; i++) {
					error = FT_Write(handle, txBuffer, 1, &byteCount);
					if (error != FT_OK) {
						printf("Error while reading from the device. Exiting.\r\n");
						return 1;
					}
				}
				time_t stopTime = clock();
				double secondsElapsed = (double)(stopTime - startTime) / CLOCKS_PER_SEC;
				double mbps = 8589.934592 / secondsElapsed;
				printf("Read 1GB from the FTDI in %0.1f seconds.\r\n", secondsElapsed);
				printf("Average read speed: %0.1f Mbps.\r\n", mbps);
				return 0;

			}
			else {

				// unable to connect or configure
				printf("Unable to connect to or configure the device. Exiting.\r\n");
				return 1;

			}
		}
	}
}