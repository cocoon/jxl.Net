# jxl.Net
jpeg-xl dotnet csharp encoder decoder wrapper and example WPF GUI
<br>Official jpeg-xl repository: https://gitlab.com/wg1/jpeg-xl/

### WARNING
This project (and the official jpeg-xl encoder / decoder) is still in its ~~very~~ early stage of development. There are bugs and missing features.

### [visit: Wiki](https://github.com/cocoon/jxl.Net/wiki)


## Getting Started

### Prerequisites
[Obtain or compile](https://github.com/cocoon/jxl.Net/wiki/Where-to-get-encoder-and-decoder-binaries) recent jpeg-xl encoder (cjxl.exe) and decoder (djxl.exe) binaries and place it into the application folder or configure the path in code.

```
EncoderOptions encOptions = new EncoderOptions
{
  EncoderPath = @"c:\dev\jxl\cjxl.exe"
};
```

Initial version used for development was v0.3.6, last tested is nightly v0.12.0 4a26aa1 (some old parameters are no longer available and are disabled, like UseNewHeuristics or new ones might be missing and would need updates).


# WPF GUI Example 
An example project is available that is using jxl.Net

<img width="1327" height="903" alt="2026-03-22_jxl_encoder_02" src="https://github.com/user-attachments/assets/ef89f262-9820-4a8a-862e-5f156837a1ed" />

## jxlViewer with slider to compare images

![jxlViewer_slider](https://user-images.githubusercontent.com/1071741/112016170-c4b5c200-8b2c-11eb-9040-24c7deea991b.jpg)
