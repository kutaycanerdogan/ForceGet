export class Quote {
  id: number;
  userId: number;
  country: string;
  city: string;
  mode: number; 
  movementType: number;
  incoterms: number;
  packageType: number;
  currency: number;
  originalAmount: number;
  convertedUSD: number;
  createdAt: Date;

  constructor(
    id: number = 0,
    userId: number = 0,
    country: string = '',
    city: string = '',
    mode: number = 0,
    movementType: number = 0,
    incoterms: number = 0,
    packageType: number = 0,
    currency: number = 0,
    originalAmount: number = 0,
    convertedUSD: number = 0,
    createdAt: Date = new Date()
  ) {
    this.id = id;
    this.userId = userId;
    this.country = country;
    this.city = city;
    this.mode = mode;
    this.movementType = movementType;
    this.incoterms = incoterms;
    this.packageType = packageType;
    this.currency = currency;
    this.originalAmount = originalAmount;
    this.convertedUSD = convertedUSD;
    this.createdAt = createdAt;
  }
}
