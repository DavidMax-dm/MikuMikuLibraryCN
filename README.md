# Miku Miku Library

专门为《初音未来：歌姬计划》系列游戏而编写的格式库与文件编辑器。

# Building

* [稳定版本](https://github.com/DavidMax-dm/MikuMikuLibrary_CN/releases)
* [开发版本](https://ci.appveyor.com/project/DavidMax-dm/mikumikulibrary-cn/build/artifacts)

## Manually building

1. Clone the repository with the `--recursive` option. `git clone --recursive https://github.com/blueskythlikesclouds/MikuMikuLibrary.git`
2. Install FBX SDK. (See instructions [here.](https://github.com/blueskythlikesclouds/MikuMikuLibrary/tree/master/MikuMikuLibrary.Native/Dependencies/FBX))
3. Install the .NET SDK/.NET 7.0 Runtime through Visual Studio Installer.
4. Open the solution in Visual Studio 2022.
5. Restore the missing NuGet packages.
6. Build the solution.

# Projects

## Miku Miku Library

解决方案的主要数据库，提供了读取、编辑与写入《初音未来：歌姬计划》系列游戏的文件格式的方法和类。

## Miku Miku Model

格式库的GUI前端，允许你编辑模型，纹理，动作与纹理。

## Command line tools

格式库的部分功能的数据区前端。


### Database Converter

一个允许你转换数据库文件至XML或者将其转换回原格式的程序。


支持文件：

* aet_db.bin/.aei
* bone_data.bin/.bon
* mot_db.bin
* obj_db.bin/.osi
* spr_db.bin/.spi
* stage_data.bin/.stg
* str_array.bin/string_array.bin/.str
* tex_db.bin/.txi

### FARC Pack

一个允许你解压或者打包FARC文件的程序。MM+的CPK文件同样也受支持。

# Special thanks

* [ActualMandM](https://github.com/ActualMandM)
* [BroGamer4256](https://github.com/BroGamer4256)
* [Brolijah](https://github.com/Brolijah)
* [Charl-Ep](https://github.com/Charl-Ep)
* [chrrox](https://www.deviantart.com/chrrox)
* [featjinsoul](https://github.com/featjinsoul)
* [keikei14](https://github.com/keikei14)
* [korenkonder](https://github.com/korenkonder)
* [lybxlpsv](https://github.com/lybxlpsv)
* [minmode](https://www.deviantart.com/minmode)
* [nastys](https://github.com/nastys)
* [s117](https://github.com/s117)
* [samyuu](https://github.com/samyuu)
* [Stewie100](https://github.com/Stewie100)
* [thtrandomlurker](https://github.com/thtrandomlurker)
* [Waelwindows](https://github.com/Waelwindows)
