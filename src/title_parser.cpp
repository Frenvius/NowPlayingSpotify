#include "title_parser.h"

#include "text.h"

namespace {

const wchar_t kSeparator[] = L" - ";
const int kSeparatorLength = 3;

const wchar_t *FindSeparator(const wchar_t *from) {
  for (const wchar_t *p = from; *p; ++p) {
    int i = 0;
    while (i < kSeparatorLength && p[i] == kSeparator[i])
      ++i;
    if (i == kSeparatorLength)
      return p;
  }
  return 0;
}

bool IsIdlePlaceholder(const wchar_t *value) {
  return Equals(value, L"Spotify Premium") || Equals(value, L"Spotify Free");
}

void AppendAlbumPart(Track *out, const wchar_t *begin, const wchar_t *end) {
  const int capacity = sizeof(out->album) / sizeof(wchar_t);
  if (out->album[0])
    Append(out->album, capacity, L" ");
  wchar_t part[256];
  CopyRange(part, sizeof(part) / sizeof(wchar_t), begin, end);
  Append(out->album, capacity, part);
}

}

void ParseWindowTitle(const wchar_t *window_title, Track *out) {
  *out = Track();

  if (IsEmpty(window_title) || Equals(window_title, L"Spotify"))
    return;

  const int artist_capacity = sizeof(out->artist) / sizeof(wchar_t);
  const int title_capacity = sizeof(out->title) / sizeof(wchar_t);

  const wchar_t *first = FindSeparator(window_title);
  if (!first) {
    Copy(out->artist, artist_capacity, window_title);
  } else {
    CopyRange(out->artist, artist_capacity, window_title, first);
    const wchar_t *song_begin = first + kSeparatorLength;
    const wchar_t *second = FindSeparator(song_begin);
    if (!second) {
      Copy(out->title, title_capacity, song_begin);
    } else {
      CopyRange(out->title, title_capacity, song_begin, second);
      const wchar_t *part = second + kSeparatorLength;
      while (const wchar_t *next = FindSeparator(part)) {
        AppendAlbumPart(out, part, next);
        part = next + kSeparatorLength;
      }
      AppendAlbumPart(out, part, part + Length(part));
    }
  }

  if (IsIdlePlaceholder(out->artist) || IsIdlePlaceholder(out->title)) {
    *out = Track();
    out->state = PlaybackIdle;
    return;
  }

  out->state = PlaybackPlaying;
}
