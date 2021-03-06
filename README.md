# CUPP - Common User Passwords Profiler
Inspired by [Mebus / cupp](https://github.com/Mebus/cupp).

## Usage
Requires three files:
- `words.txt` line-separated list of strings such as name, address, nicknames, etc.
- `numbers.txt` line-separated list of numbers such as day/month/year of birth, favorite number, etc.
- `chars.txt` line-separated list of special characters such as underscore, at sign, etc.

```
> CUPP.exe [args]

Args:
- [0], --min length
- [1], --max length
- i, --invert passwords (split at special char)
- s, --include special char
- c, --include first-letter capitalization 
- n, --include numbers

Example:
> CUPP.exe 6 20 s n
```

## To do
- [x] Parameters (min length, max length, numeric only, alphanumeric, etc.)
- [ ] Include file with most used passwords (parameter)
- [ ] Optimize algorithm with special chars at start/end of string, etc
- [ ] Add tests
