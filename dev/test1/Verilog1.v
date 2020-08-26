

module top(
	CLK,
	LEDs,
	RST
	);
	
	output reg [7:0] LEDs;
	input CLK;
	input RST;

	
	always @ (posedge CLK, negedge RST) begin
		if(!RST)
			LEDs <= 8'b11111111;
		else begin
		LEDs <= !LEDs;
		end
	end
	
endmodule