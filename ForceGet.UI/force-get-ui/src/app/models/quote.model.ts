import {
  Mode,
  MovementType,
  Incoterm,
  PackageType,
  CurrencyType,
} from '../models/enums.model';

export class Quote {
  id: number = 0;
  userId: number = 0;
  country: string = '';
  city: string = '';
  mode: Mode = 0;
  movementType: MovementType = 0;
  incoterms: Incoterm = 0;
  packageType: PackageType = 0;
  fromCurrency: CurrencyType = 0;
  originalAmount: number = 0;
  toCurrency: CurrencyType = 0;
  convertedAmount: number = 0;
  createdAt: Date = new Date();
  user?: any | null;
}
