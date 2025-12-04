export class Country {
  name: string;
  currencyCode: string;

  constructor(name: string = '', currencyCode: string = '') {
    this.name = name;
    this.currencyCode = currencyCode;
  }
}
