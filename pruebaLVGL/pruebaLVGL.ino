#include "Display_ST7789.h"
#include "LVGL_Driver.h"
#include "ui.h"
#include <Adafruit_NeoPixel.h>

#define NUM_LEDS 1
#define DATA_PIN 8 // Pin de RBG en la placa

Adafruit_NeoPixel strip = Adafruit_NeoPixel(NUM_LEDS, DATA_PIN, NEO_GRB + NEO_KHZ800);

void setup()
{       
  LCD_Init();
  Lvgl_Init();
  ui_init();
  RBG_init();  
}

void loop()
{
  Timer_Loop();
  rainbowEffect();
  delay(5);
}

void RBG_init()
{
  strip.begin();
  strip.show();
}

void rainbowEffect() {
  static uint16_t j = 0;
  for(uint16_t i=0; i<strip.numPixels(); i++) {
    strip.setPixelColor(i, Wheel((i+j) & 255));
  }
  strip.show();
  j++;
  delay(20); // Ajusta la velocidad del efecto
}

// Input a value 0 to 255 to get a color value.
// The colours are a transition r - g - b - back to r.
uint32_t Wheel(byte WheelPos) {
  WheelPos = 255 - WheelPos;
  if(WheelPos < 85) {
    return strip.Color(255 - WheelPos * 3, 0, WheelPos * 3);
  }
  if(WheelPos < 170) {
    WheelPos -= 85;
    return strip.Color(0, WheelPos * 3, 255 - WheelPos * 3);
  }
  WheelPos -= 170;
  return strip.Color(WheelPos * 3, 255 - WheelPos * 3, 0);
}
