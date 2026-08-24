#pragma once

inline int Length(const wchar_t *s) {
  int n = 0;
  while (s && s[n])
    ++n;
  return n;
}

inline void CopyRange(wchar_t *dst, int capacity, const wchar_t *begin,
                      const wchar_t *end) {
  if (capacity <= 0)
    return;
  int i = 0;
  while (begin < end && i < capacity - 1)
    dst[i++] = *begin++;
  dst[i] = 0;
}

inline void Copy(wchar_t *dst, int capacity, const wchar_t *src) {
  CopyRange(dst, capacity, src, src + Length(src));
}

inline bool Equals(const wchar_t *a, const wchar_t *b) {
  if (a == b)
    return true;
  if (!a || !b)
    return false;
  while (*a && *a == *b) {
    ++a;
    ++b;
  }
  return *a == *b;
}

inline bool IsEmpty(const wchar_t *s) { return !s || !s[0]; }

inline void Append(wchar_t *dst, int capacity, const wchar_t *src) {
  int at = Length(dst);
  int i = 0;
  while (src[i] && at + i < capacity - 1) {
    dst[at + i] = src[i];
    ++i;
  }
  dst[at + i] = 0;
}
