`timescale 1ns / 1ps

module top_tb;
	

	reg CLK;
	reg START;
	reg RST;
	wire I2S_WORD_SELECT;
    wire I2S_DATA_OUT;
    wire I2S_CLK_OUT;
    

	top uut (
    .fpga_clk(CLK),
    .nrst(RST),
    .I2S_word_select(I2S_WORD_SELECT),
    .I2S_data_out(I2S_DATA_OUT),
    .I2S_clk_out(I2S_CLK_OUT)
    );

	
	initial begin
		$dumpfile("top_test.vcd");
		$dumpvars();
		CLK = 1;
		RST = 0;
		
	end
	
	initial begin
		wait(!CLK);
		#1
		RST = 0;
		#3
		RST = 1;
		
        


		
	end
	
	
	
	initial begin
        #3000 $finish;
    end

    always
        #2  CLK =  ! CLK;

endmodule
