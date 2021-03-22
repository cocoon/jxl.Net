# jxl.Net
jpeg-xl dotnet csharp encoder decoder wrapper and example WPF GUI
<br>Official jpeg-xl repository: https://gitlab.com/wg1/jpeg-xl/

### WARNING
This project (and the official jpeg-xl encoder / decoder) is still in its very early stage of development. There are bugs and missing features.


## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites
[Obtain or compile](https://github.com/cocoon/jxl.Net/wiki/Where-to-get-encoder-and-decoder-binaries) recent jpeg-xl encoder (cjxl.exe) and decoder (djxl.exe) binaries and place it into the application folder or configure the path in code.

```
EncoderOptions encOptions = new EncoderOptions
{
  EncoderPath = @"c:\dev\jxl\cjxl.exe"
};
```

Current version used was v0.3.2.


# WPF GUI Example 
An example project is available that is using jxl.Net

![jxl NET-WPF-GUI_2021-03-22](https://user-images.githubusercontent.com/1071741/111987765-e6548080-8b0f-11eb-8c10-0494fcb6802e.jpg)

## jxlViewer with slider to compare images

![jxlViewer_slider](https://user-images.githubusercontent.com/1071741/112016170-c4b5c200-8b2c-11eb-9040-24c7deea991b.jpg)
