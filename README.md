## 青海地学旅游后台管理客户端

通过CefSharp嵌入web管理程序，但媒体文件等批量上传由C#本地代码实现，通过CefSharp注入外部对象，web和本地相互调用\*^\*


### 环境
开发环境Visual Studio 2017+
web程序在单独的[仓库](https://github.com/mattuylee/QingHaiGeoMS.git)，需要打包好后放到程序运行目录下`web/`路径
视频压缩需要ffmpeg，要么本地安装然后配置好环境变量，要么将ffmpeg程序目录放到本程序运行目录下`ffmpeg/`，程序自动检测