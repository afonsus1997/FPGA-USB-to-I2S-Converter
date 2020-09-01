################################################################################
# Automatically-generated file. Do not edit!
################################################################################

# Add inputs and outputs from these tool invocations to the build variables 
C_SRCS += \
../Middlewares/ST/STM32_USB_Host_Library/Class/MTP/Src/usbh_mtp.c \
../Middlewares/ST/STM32_USB_Host_Library/Class/MTP/Src/usbh_mtp_ptp.c 

OBJS += \
./Middlewares/ST/STM32_USB_Host_Library/Class/MTP/Src/usbh_mtp.o \
./Middlewares/ST/STM32_USB_Host_Library/Class/MTP/Src/usbh_mtp_ptp.o 

C_DEPS += \
./Middlewares/ST/STM32_USB_Host_Library/Class/MTP/Src/usbh_mtp.d \
./Middlewares/ST/STM32_USB_Host_Library/Class/MTP/Src/usbh_mtp_ptp.d 


# Each subdirectory must supply rules for building sources it contributes
Middlewares/ST/STM32_USB_Host_Library/Class/MTP/Src/usbh_mtp.o: ../Middlewares/ST/STM32_USB_Host_Library/Class/MTP/Src/usbh_mtp.c
	arm-none-eabi-gcc "$<" -mcpu=cortex-m4 -std=gnu11 -g3 -DUSE_HAL_DRIVER -DSTM32F412Zx -DDEBUG -c -I../Inc -I../Drivers/CMSIS/Include -I../Middlewares/ST/STM32_USB_Host_Library/Class/MTP/Inc -I../Drivers/STM32F4xx_HAL_Driver/Inc -I../Drivers/CMSIS/Device/ST/STM32F4xx/Include -I../Drivers/STM32F4xx_HAL_Driver/Inc/Legacy -I../Middlewares/ST/STM32_USB_Host_Library/Core/Inc -O0 -ffunction-sections -fdata-sections -Wall -fstack-usage -MMD -MP -MF"Middlewares/ST/STM32_USB_Host_Library/Class/MTP/Src/usbh_mtp.d" -MT"$@" --specs=nano.specs -mfpu=fpv4-sp-d16 -mfloat-abi=hard -mthumb -o "$@"
Middlewares/ST/STM32_USB_Host_Library/Class/MTP/Src/usbh_mtp_ptp.o: ../Middlewares/ST/STM32_USB_Host_Library/Class/MTP/Src/usbh_mtp_ptp.c
	arm-none-eabi-gcc "$<" -mcpu=cortex-m4 -std=gnu11 -g3 -DUSE_HAL_DRIVER -DSTM32F412Zx -DDEBUG -c -I../Inc -I../Drivers/CMSIS/Include -I../Middlewares/ST/STM32_USB_Host_Library/Class/MTP/Inc -I../Drivers/STM32F4xx_HAL_Driver/Inc -I../Drivers/CMSIS/Device/ST/STM32F4xx/Include -I../Drivers/STM32F4xx_HAL_Driver/Inc/Legacy -I../Middlewares/ST/STM32_USB_Host_Library/Core/Inc -O0 -ffunction-sections -fdata-sections -Wall -fstack-usage -MMD -MP -MF"Middlewares/ST/STM32_USB_Host_Library/Class/MTP/Src/usbh_mtp_ptp.d" -MT"$@" --specs=nano.specs -mfpu=fpv4-sp-d16 -mfloat-abi=hard -mthumb -o "$@"

