shift_register_test:
	@iverilog I2S_shift_register_tb.v I2S_shift_register.v -o I2S_shift_register_test.out
	@./I2S_shift_register_test.out 
	@gtkwave I2S_shift_register_test.vcd gtk_I2S_shift_register_test.gtkw &

clean:
	@rm *.out *.vcd
	$(info All clean!)
