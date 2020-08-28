//for 32bit 32,22,2,1

module sample_generator #
(   
    parameter NBIT = 32
) (
    input clk,
    input rst,
    input [NBIT-1:0] seed, //this should be replaced after testing with a parameter
    output reg [NBIT-1:0] out
);



    // assign out[0] = dff[0];
    // assign out[1] = dff[1];
    // assign out[2] = dff[2];
    // assign out[3] = dff[3];
    




    always @ (posedge clk) begin
        if(rst) begin
            out <= seed;
        end
        else begin
            out <= {out[NBIT-2:0], out[31] ^ out[22] ^ out[1] ^ out[0]};
        end

    end

endmodule