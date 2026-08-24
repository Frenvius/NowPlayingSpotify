#pragma once

enum PlaybackState { PlaybackUnavailable, PlaybackIdle, PlaybackPlaying };

struct Track {
  PlaybackState state;
  wchar_t artist[192];
  wchar_t title[192];
  wchar_t album[256];

  Track() : state(PlaybackUnavailable) {
    artist[0] = 0;
    title[0] = 0;
    album[0] = 0;
  }
};
