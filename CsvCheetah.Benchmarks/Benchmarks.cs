using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using tobixdev.github.io.CsvCheetah.Mapping;
using tobixdev.github.io.CsvCheetah.Mapping.Conversion;
using tobixdev.github.io.CsvCheetah.Mapping.Mappers;
using tobixdev.github.io.CsvCheetah.Mapping.Maps;
using tobixdev.github.io.CsvCheetah.Tokenization;
using tobixdev.github.io.CsvCheetah.Tokenization.StateMachine;

namespace CsvCheetah.Benchmarks
{
    public class Benchmarks
    {
        private const int LineCount = 1_000_000;
        private const string Line = @"hello,hello2,""thiiiiiiiiiiiisss is a
longer column"",but, there, is still more,
";
        
        private readonly string _tempFilePath = Path.Combine(Path.GetTempPath(), Path.GetTempFileName());
        
        private ITokenizer _tokenizer;
        private ITokenStreamMapper<BenchmarkDataClass> _mapper;

        [GlobalSetup]
        public async Task WriteCsvFile()
        {
            await using var fileStream = new FileStream(_tempFilePath, FileMode.Create);
            await using var fileWriter = new StreamWriter(fileStream);

            for (var i = 0; i < LineCount; i++)
                await fileWriter.WriteAsync(Line);

            var stateMachine = new TokenizerStateMachine(StateHolder.DefaultConfiguration);
            _tokenizer = new StateMachineTokenizer(stateMachine);
            
            var map = new ColumnMapBuilder<BenchmarkDataClass>()
                .WithColumn(0, c => c.Field1)
                .WithColumn(1, c => c.Field2)
                .WithColumn(2, c => c.Field3)
                .WithColumn(3, c => c.Field4)
                .WithColumn(4, c => c.Field5)
                .WithColumn(5, c => c.Field6)
                .WithColumn(6, c => c.Field7)
                .Build();
            _mapper = new MapperFactory<BenchmarkDataClass>(ConverterRegistry.CreateDefaultInstance()).CreateForMap(map);
        }

        [GlobalCleanup]
        public void DeleteTempCsvFile()
        {
            File.Delete(_tempFilePath);
        }

        [Benchmark]
        public void Tokenize()
        {
            using var fileStream = File.Open(_tempFilePath, FileMode.Open);
            using var fileReader = new StreamReader(fileStream);

            _tokenizer.Tokenize(fileReader).Count();
        }

        [Benchmark]
        public void TokenizeAndMap()
        {
            using var fileStream = File.Open(_tempFilePath, FileMode.Open);
            using var fileReader = new StreamReader(fileStream);

            var tokens = _tokenizer.Tokenize(fileReader);
            
            _mapper.Map(tokens).Count();
        }
        
    }
}