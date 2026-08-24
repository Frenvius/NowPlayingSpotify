#pragma once

#include <windows.h>

#include "track.h"

class MainWindow {
public:
  MainWindow();
  ~MainWindow();

  bool Create(HINSTANCE instance);

private:
  static LRESULT CALLBACK WindowProc(HWND window, UINT message, WPARAM wp,
                                     LPARAM lp);
  LRESULT HandleMessage(UINT message, WPARAM wp, LPARAM lp);

  void Poll();
  void Paint(HDC target) const;
  void PaintContent(HDC canvas) const;
  void OnLeftClick(LPARAM lp);
  void OnTrayMessage(WPARAM wp, LPARAM lp);
  void ShowTrayMenu() const;
  void Quit();

  HWND window_;
  NOTIFYICONDATAW tray_;
  bool tray_added_;
  HFONT header_font_;
  HFONT song_font_;
  HFONT artist_font_;
  Track track_;
};
