- Build OpenCV on your machine or get a copy of OpenCV
- Copy libraries and includes in `ThirdParty/OpenCv`

![Library files](img/lib_files.jpg)

# Conflicts

You may have to modify OpenCV includes to get it to compile. For instance, OpenCV also has a `check` macro that you can rename to avoid conflicts with Unreal macro.