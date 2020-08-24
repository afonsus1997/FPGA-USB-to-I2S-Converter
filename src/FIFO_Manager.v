


module FIFO_Manager(
	inout [7:0] USB_IO,
	input global_rst,
	output RD,
	output WR,
	input RXF,
	input TXE,
	input PWREN,
	output RST
	);
	
	assign RST = global_rst;
	
	always @ (posedge global_rst) begin
	
	
	
	end
	

	
endmodule
