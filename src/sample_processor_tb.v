`timescale 1ns / 1ps

module I2S_shift_register_tb;
	parameter S_8BIT=0, S_12BIT=1, S_16BIT=3, S_32BIT=4;
	parameter BLOCK0 = 8'hAA;
	parameter BLOCK1 = 8'hFF;
	parameter SAMPLE_SIZE_VALUE = S_12BIT;

	reg CLK;
	reg START;
	reg RST;
    reg [7:0] DATA_IN;
    reg SAMPLE_SIZE;
	wire DATA_READY;
    wire [31:0] DATA_OUT;
    

	sample_processor uut (
    .clk(CLK),
    .rst(RST),
    .data_in(DATA_IN),
    .data_out(DATA_OUT),
    .sample_size(SAMPLE_SIZE),
    .data_available(START),
    .data_ready(DATA_READY)
);

	
	initial begin
		$dumpfile("sample_processor_test.vcd");
		$dumpvars();
		CLK = 0;
		START = 1;
		RST = 0;
		DATA_IN = BLOCK0;
        SAMPLE_SIZE = S_12BIT;
		
	end
	
	initial begin
		wait(!CLK);
		#1
		RST = 1;
		#3
		RST = 0;
		
		wait(!CLK);
		#1
		START = 0;
		#3
		START = 1;
        
        DATA_IN = BLOCK1;
        #10

        wait(!CLK);
		#1
		START = 0;
		#3
		START = 1;


		
	end
	
	
	
	initial begin
        #3000 $finish;
    end

    always
        #2  CLK =  ! CLK;

endmodule
