export class Currency {
  oldAmount: number;
  oldCurrency: string;
  newCurrency: string;
  newAmount: number;

  constructor(
    oldAmount: number = 0,
    oldCurrency: string = '',
    newCurrency: string = '',
    newAmount: number = 0
  ) {
    this.oldAmount = oldAmount;
    this.oldCurrency = oldCurrency;
    this.newCurrency = newCurrency;
    this.newAmount = newAmount;
  }
}
