# CsvCheetah

## Motivation

This is the result of a experiment I made out of sheer curiosity. I wanted to know how the performance compares to the popular library [CsvHelper](https://github.com/search?q=CsvHelper).

Keep in mind that CsvHelper has more features and that there may be more recent versions.

## Results

For the given Benchmark (One million "Csv-Lines") CsvHelper took ~3.1 seconds on my machine. The same operation was performed from CsvCheetah in ~1.8 seconds.

Feel free to try it on your machine, if you're curious.

## Further Development

I probably will add some features here and there if I have time & motivation.

Some features missing:

* Parse header lines. Currently only mappings by column index is supported.
* Add more default type converters.
* Add more configuration possibilities
 * Custom Record Separators
 * ChunkSize 
* Async APIs
 * IAsyncEnumerable 
* Facade similar to CsvHelpers `CsvReader` class, which abstracts many things from the user (for simple cases)
* Map to dynamic
* ... 