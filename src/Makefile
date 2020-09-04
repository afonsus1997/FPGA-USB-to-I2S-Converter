shift_register_test:
	@iverilog I2S_shift_register_tb.v I2S_shift_register.v -o I2S_shift_register_test.out
	@./I2S_shift_register_test.out 
	@gtkwave I2S_shift_register_test.vcd gtk_I2S_shift_register_test.gtkw &

sample_processor_test:
	@iverilog sample_processor_tb.v sample_processor.v -o sample_processor_test.out
	@./sample_processor_test.out 
	@gtkwave sample_processor_test.vcd gtk_sample_processor_test.gtkw &

sample_generator_test:
	@iverilog sample_generator_tb.v sample_generator.v -o sample_generator_test.out
	@./sample_generator_test.out 

top_test:
	@iverilog top_tb.v top.v audio_clock_mux.v sample_generator.v sample_processor.v I2S_shift_register.v -o top_test.out
	./top_test.out
	@gtkwave top_test.vcd gtk_top_test.gtkw &

clean:
	@rm *.out *.vcd
	$(info All clean!)
