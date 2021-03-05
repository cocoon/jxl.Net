# jxl.Net
jpeg-xl dotnet csharp encoder decoder wrapper and example WPF GUI
<br>Official jpeg-xl repository: https://gitlab.com/wg1/jpeg-xl/

### WARNING
This project (and the official jpeg-xl encoder / decoder) is still in its very early stage of development. There are bugs and missing features.


## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

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

![jxl NET-WPF-GUI](https://user-images.githubusercontent.com/1071741/110150179-3bd62100-7ddf-11eb-8ff8-6d3728de45e6.jpg)
