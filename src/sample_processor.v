module sample_processor (
    input clk,
    input rst,
    input [7:0] data_in,
    output [31:0] data_out,
    input [3:0] sample_size,
    input data_available,
    output reg data_ready
);
    parameter S_8BIT=0, S_12BIT=1, S_16BIT=3, S_24BIT=4, S_32BIT=5;
    parameter S_BLOCK_8BIT=1, S_BLOCK_12BIT=2, S_BLOCK_16BIT=2, S_BLOCK_24BIT=3, S_BLOCK_32BIT=4;
    parameter S_MASK_8BIT=32'hFF, S_MASK_12BIT=32'hFFF, S_MASK_16BIT=32'hFFFF, S_MASK_24BIT=32'hFFFFF, S_MASK_32BIT=32'hFFFFFFFF;

    reg [3:0] current_block_n;
    reg [7:0] sample_blocks [3:0];
    reg [3:0] n_blocks_per_sample;
    reg [31:0] sample_mask;

    assign data_out = {sample_blocks[3], sample_blocks[2], sample_blocks[1], sample_blocks[0]} & sample_mask;

    always @(*) begin
		case(sample_size)
			S_8BIT : n_blocks_per_sample <= S_BLOCK_8BIT;
			S_12BIT : n_blocks_per_sample <= S_BLOCK_12BIT;
			S_16BIT : n_blocks_per_sample <= S_BLOCK_16BIT;
			S_24BIT : n_blocks_per_sample <= S_BLOCK_24BIT;
			S_32BIT : n_blocks_per_sample <= S_BLOCK_32BIT;
		endcase
	end

    always @(*) begin
		case(sample_size)
			S_8BIT : sample_mask <= S_MASK_8BIT;
			S_12BIT : sample_mask <= S_MASK_12BIT;
			S_16BIT : sample_mask <= S_MASK_16BIT;
			S_24BIT : sample_mask <= S_MASK_24BIT;
			S_32BIT : sample_mask <= S_MASK_32BIT;
		endcase
	end


    always @ (posedge clk) begin
        if(rst) begin
            data_ready <= 0;
            current_block_n <= 0;
            sample_blocks[0] = 0;
            sample_blocks[1] = 0;
            sample_blocks[2] = 0;
            sample_blocks[3] = 0;
        end

    end

    always @ (negedge data_available) begin

        if(current_block_n == n_blocks_per_sample) begin
            data_ready <= 1;
        end
        else begin
            sample_blocks[current_block_n] <= data_in;
            current_block_n <= current_block_n + 1;
        end
                
    end

endmodule

//dff <= {dff[4-2:0], dff[3] ^ dff[2] ^ scan_in};