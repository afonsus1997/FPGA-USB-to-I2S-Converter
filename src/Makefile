shift_register_test:
	@iverilog I2S_shift_register_tb.v I2S_shift_register.v -o I2S_shift_register_test.out
	@./I2S_shift_register_test.out 
	@gtkwave I2S_shift_register_test.vcd gtk_I2S_shift_register_test.gtkw &

sample_processor_test:
	@iverilog sample_processor_tb.v sample_processor.v -o sample_processor_test.out
	@./sample_processor_test.out 
	@gtkwave sample_processor_test.vcd gtk_sample_processor_test.gtkw &

clean:
	@rm *.out *.vcd
	$(info All clean!)
