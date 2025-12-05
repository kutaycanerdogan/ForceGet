export class CurrencyConversion {
  id: number = 0;
  userId: number = 0;
  fromCurrency: string = '';
  toCurrency: string = '';
  originalAmount: number = 0;
  convertedAmount: number = 0;
  convertedAt: Date = new Date();
}
