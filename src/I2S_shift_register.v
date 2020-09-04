module shift_register(
	input clk,
	input [31:0] sample_left,
	input [31:0] sample_right,
	input [3:0] sample_size,
	input start,
	input rst,
	output reg busy_right,
	output reg busy_left,
	output word_select,
	output reg data_out,
	output reg clk_out
	);
	
	parameter IDLE_s=0, START_s=1, RUNNING_s=3;
	parameter S_8BIT=0, S_12BIT=1, S_16BIT=3, S_24BIT=4, S_32BIT=5;
	parameter LEFT=0, RIGHT=1;
	
	reg [7:0] sample_left_r;
	reg [7:0] sample_right_r;
	reg [3:0] state;
	reg [3:0] next_state;
	reg [7:0] bit_counter_right;
	reg [7:0] bit_counter_left;
	reg [7:0] counter_size;
	reg current_out;
	
	assign word_select = current_out;
	
	always @ (posedge clk) begin
		case(sample_size)
				S_8BIT  : counter_size <= 8'd8;
				S_12BIT : counter_size <= 8'd12;
				S_16BIT : counter_size <= 8'd16;
				S_32BIT : counter_size <= 8'd32;
			endcase
		end
	
	always @ (posedge clk) begin
		if(rst) begin
			state       <= IDLE_s;
		end
		else
			state       <= next_state;
	end
	
	always @(*) begin
		case (state)
			IDLE_s:
				if(start)
					next_state = START_s;
				else
					next_state = IDLE_s;
			START_s:
				next_state = RUNNING_s;
			RUNNING_s:
				next_state = RUNNING_s;
			default:
				next_state  = IDLE_s;
		endcase
	end
	
	
	
	always @ (posedge clk) begin
		if(rst) begin
		sample_left_r <= 0;
		sample_right_r <= 0;
		busy_right <= 0;
		busy_left <= 0;
//		word_select <= 0;
		data_out <= 0;
		clk_out <= 0;
		current_out <= 0;
		end
		
		else if(state == START_s) begin
			sample_left_r <= sample_left;
			sample_right_r <= sample_right;
			
			bit_counter_right <= counter_size+1;
			bit_counter_left <= counter_size+1;
		end
		
		else if(state == RUNNING_s) begin
		
			if(current_out == LEFT) begin
				if(bit_counter_left != 0) begin
				//shift bit to output
					data_out <= sample_left_r[0];
					sample_left_r <= sample_left_r >> 1;
					bit_counter_left <= bit_counter_left - 1;
				end
				else begin
					current_out <= RIGHT;
					sample_right_r <= sample_right;
					bit_counter_left <= counter_size;
				end
			end
			else if(current_out == RIGHT) begin
				if(bit_counter_right != 0) begin
				//shift bit to output
					data_out <= sample_right_r[0];
					sample_right_r <= sample_right_r >> 1;
					bit_counter_right <= bit_counter_right - 1;
				end
				else begin
					current_out <= LEFT;
					sample_left_r <= sample_left;
					bit_counter_right <= counter_size;
				end
				
			end
		
		end
		
		
	end
	
	
	
endmodule
	