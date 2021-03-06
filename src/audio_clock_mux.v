


module audio_clock_mux (
    input clk_48k,
    input clk_88_2k,
    input clk_96k,
    input [3:0] clk_sel,
    output clk_out
);

    parameter s_48k = 0, s_88_2k = 1, s_96k = 2;

    assign clk_out = ( clk_sel == s_48k )? clk_48k : ( clk_sel == s_88_2k )? clk_88_2k : ( clk_sel == s_96k )? clk_96k : clk_48k;
    
endmodule