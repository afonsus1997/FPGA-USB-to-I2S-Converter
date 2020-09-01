

//`define debug


module top (
    input fpga_clk,
    input nrst,
	 input [7:0] FIFO_IO,
	 input FIFO_RXF,
	 input FIFO_TXE,
	 input FIFO_PWREN,
	 output reg [7:0] LED,
	 output FIFO_RD,
	 output FIFO_WR,
	 output FIFO_RST,
	 output I2S_word_select,
	 output I2S_data_out,
	 output I2S_clk_out
	 
	 
);
    
    parameter s_48k = 0, s_88_2k = 1, s_96k = 2;
    parameter SEED = 32'd10;
    parameter S_8BIT=0, S_12BIT=1, S_16BIT=3, S_24BIT=4, S_32BIT=5;
    parameter SAMPLE_SIZE = S_16BIT;
	 
	 wire rst;
//    wire fpga_clk;
    wire main_clk;
    wire audio_clks [2:0];
    wire audio_clk_sel;
    wire audio_clk_out;
    wire main_rst;
    wire sample_gen_out;
    wire sample_processor_out;
//    wire I2S_clk_out;
//    wire I2S_word_select;
//    wire I2S_data_out;
    wire data_stream_start;
	 
	 assign rst = ~nrst;

    `ifndef debug
    PLL pll1(
        .inclk0(fpga_clk),
        .c0(main_clk),
        .c1(audio_clks[s_48k]),
        .c2(audio_clks[s_88_2k])
    );

    PLL2 pll2(
        .inclk0(fpga_clk),
        .c0(audio_clks[s_96k])
    );
    

    audio_clock_mux clk_mux(
        .clk_48k(audio_clks[s_48k]),
        .clk_88_2k(audio_clks[s_88_2k]),
        .clk_96k(audio_clks[s_96k]),
        .clk_sel(audio_clk_sel),
        .clk_out(audio_clk_out)
    );
    `endif

    `ifdef debug
    assign main_clk = fpga_clk;
    assign audio_clk_out = main_clk;
    `endif

    sample_generator sample_generator1(
        .clk(audio_clk_out),
        .rst(main_rst),
        .seed(SEED),
        .out(sample_gen_out)
    );

    sample_processor sample_processor1(
        .clk(audio_clk_out),
        .rst(main_rst),
        .data_in(sample_gen_out),
        .data_out(sample_processor_out),
        .sample_size(SAMPLE_SIZE),
        .data_available(),
        .data_ready(data_stream_start),
    );


    shift_register I2S_shift_register1(
        .clk(audio_clk_out),
        .sample_left(sample_processor_out),
        .sample_right(sample_processor_out),
        .sample_size(SAMPLE_SIZE),
        .start(data_stream_start),
        .rst(main_rst),
        // .busy_left(),
        // .busy_right(),
        .word_select(I2S_word_select),
        .data_out(I2S_data_out),
        .clk_out(I2S_clk_out)
    );


endmodule