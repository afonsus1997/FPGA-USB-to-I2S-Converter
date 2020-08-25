`timescale 1ns / 1ps

module I2S_shift_register_tb;
	parameter S_8BIT=0, S_12BIT=1, S_16BIT=3, S_32BIT=4;
	parameter SAMPLE_LEFT_VALUE = 145;
	parameter SAMPLE_RIGHT_VALUE = 145;
	parameter SAMPLE_SIZE_VALUE = S_8BIT;

	reg CLK;
	reg START;
	reg RST;
	reg [32:0] SAMPLE_LEFT;
	reg [32:0] SAMPLE_RIGHT;
	reg [3:0] SAMPLE_SIZE;
	wire WORD_SELECT;
	wire DATA_OUT;

	shift_register uut(
		.clk(CLK),
		.sample_left(SAMPLE_LEFT),
		.sample_right(SAMPLE_RIGHT),
		.sample_size(SAMPLE_SIZE),
		.start(START),
		.rst(RST),
		//.busy_right(),
		//.busy_left(),
		.word_select(WORD_SELECT),
		.data_out(DATA_OUT)
		//.clk_out()
		
	);

	
	initial begin
		$dumpfile("I2S_shift_register_test.vcd");
		$dumpvars();
		CLK = 0;
		START = 0;
		RST = 0;
		SAMPLE_LEFT = SAMPLE_LEFT_VALUE;
		SAMPLE_RIGHT = SAMPLE_RIGHT_VALUE;
		SAMPLE_SIZE = SAMPLE_SIZE_VALUE;
	end
	
	initial begin
		wait(!CLK);
		#1
		RST = 1;
		#3
		RST = 0;
		
		wait(!CLK);
		#1
		START = 1;
		#3
		START = 0;
		
	end
	
	
	
	initial begin
        #3000 $finish;
    end

    always
        #2  CLK =  ! CLK;

endmodule
