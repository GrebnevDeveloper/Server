using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Output
    {
        public decimal SumResult { get; set; }
        public int MulResult { get; set; }
        public decimal[] SortedInputs { get; set; }
        public Output()
        { }
        public Output(Input input)
        {
            SumResult = calculationSumResult(input);
            MulResult = calculationMulResult(input);
            SortedInputs = calculationSortedResult(input);
        }
        private decimal calculationSumResult(Input input)
        {
            decimal sumResult = 0;
            for (int i = 0; i < input.Sums.Length; i++)
            {
                sumResult += input.Sums[i];
            }
            sumResult *= input.K;
            return sumResult;
        }
        private int calculationMulResult(Input input)
        {
            int mulResult = 0;
            mulResult = input.Muls[0];
            for (int i = 1; i < input.Muls.Length; i++)
            {
                mulResult *= input.Muls[i];
            }

            return mulResult;
        }
        private decimal[] calculationSortedResult(Input input)
        {
            decimal[] sortedInputs = new decimal[input.Sums.Length + input.Muls.Length];
            for (int i = 0; i < input.Sums.Length; i++)
            {
                sortedInputs[i] = input.Sums[i];
            }
            for (int i = input.Sums.Length; i < input.Sums.Length + input.Muls.Length; i++)
            {
                sortedInputs[i] = (decimal)input.Muls[i - input.Sums.Length];
            }
            Array.Sort(sortedInputs);
            return sortedInputs;
        }
    }
}
