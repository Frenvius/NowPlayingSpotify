MINGW ?= D:/tools/mingw32/bin
CXX := $(MINGW)/g++
WINDRES := $(MINGW)/windres

BUILD := build
TARGET := $(BUILD)/NowPlayingSpotify.exe

# Windows XP is the floor: 0x0501 for the API surface, subsystem 5.01 so the loader accepts the
# image, and no libstdc++ because it pulls in Vista-only time zone entry points.
XP_FLAGS := -D_WIN32_WINNT=0x0501 -DWINVER=0x0501 -DNTDDI_VERSION=0x05010000
CXXFLAGS := -Isrc -O2 -municode -fno-exceptions -fno-rtti -nostdlib++ -Wall -Wextra $(XP_FLAGS)
LDFLAGS := -mwindows -municode -static -static-libgcc -s \
           -Wl,--major-subsystem-version=5 -Wl,--minor-subsystem-version=1 \
           -Wl,--major-os-version=5 -Wl,--minor-os-version=1
LIBS := -luser32 -lgdi32 -lshell32 -lkernel32 -ladvapi32

SOURCES := \
    src/main.cpp \
    src/messenger.cpp \
    src/spotify.cpp \
    src/startup.cpp \
    src/title_parser.cpp \
    src/window.cpp

OBJECTS := $(SOURCES:%.cpp=$(BUILD)/%.o) $(BUILD)/src/app.o

all: $(TARGET)

$(TARGET): $(OBJECTS)
	@mkdir -p $(dir $@)
	$(CXX) $(OBJECTS) -o $@ $(LDFLAGS) $(LIBS)

$(BUILD)/%.o: %.cpp
	@mkdir -p $(dir $@)
	$(CXX) $(CXXFLAGS) -c $< -o $@

$(BUILD)/src/app.o: src/app.rc src/resource.h icon.ico
	@mkdir -p $(dir $@)
	$(WINDRES) -Isrc -i src/app.rc -o $@

clean:
	rm -rf $(BUILD)

.PHONY: all clean
