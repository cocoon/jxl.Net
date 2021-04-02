# jxl.Net
jpeg-xl dotnet csharp encoder decoder wrapper and example WPF GUI
<br>Official jpeg-xl repository: https://gitlab.com/wg1/jpeg-xl/

### WARNING
This project (and the official jpeg-xl encoder / decoder) is still in its very early stage of development. There are bugs and missing features.

### [visit: Wiki](https://github.com/cocoon/jxl.Net/wiki)


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

Current version used was v0.3.6.


# WPF GUI Example 
An example project is available that is using jxl.Net

![2021-03-27_WPF_UI_with_ImageComparison_DarkMode](https://user-images.githubusercontent.com/1071741/112734453-25d30080-8f46-11eb-8c63-689ad52f1707.png)

## jxlViewer with slider to compare images

![jxlViewer_slider](https://user-images.githubusercontent.com/1071741/112016170-c4b5c200-8b2c-11eb-9040-24c7deea991b.jpg)
