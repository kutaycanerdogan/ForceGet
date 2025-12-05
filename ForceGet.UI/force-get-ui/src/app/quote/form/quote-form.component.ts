import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { QuoteService } from '../../services/quote.service';
import { CountryService } from '../../services/country.service';
import { CurrencyService } from '../../services/currency.service';
import { debounceTime, distinctUntilChanged, switchMap } from 'rxjs/operators';
import {
  Mode,
  MovementType,
  Incoterm,
  PackageType,
  CurrencyType,
} from '../../models/enums.model';
import { Country } from '../../models/country.model';
import { CurrencyConversion } from '../../models/currency-converstaion.model';

@Component({
  selector: 'app-quote-form',
  templateUrl: './quote-form.component.html',
  styleUrls: ['./quote-form.component.scss'],
})
export class QuoteFormComponent implements OnInit {
  quoteForm: FormGroup;

  Mode = Mode;
  MovementType = MovementType;
  Incoterm = Incoterm;
  PackageType = PackageType;
  CurrencyType = CurrencyType;

  modes = Object.keys(Mode).filter((key) =>
    isNaN(Number(key))
  ) as (keyof typeof Mode)[];
  movementTypes = Object.keys(MovementType).filter((key) =>
    isNaN(Number(key))
  ) as (keyof typeof MovementType)[];

  incoterms = Object.keys(Incoterm).filter((key) =>
    isNaN(Number(key))
  ) as (keyof typeof Incoterm)[];

  packageTypes = Object.keys(PackageType).filter((key) =>
    isNaN(Number(key))
  ) as (keyof typeof PackageType)[];

  currencies = Object.keys(CurrencyType).filter((key) =>
    isNaN(Number(key))
  ) as (keyof typeof CurrencyType)[];

  filteredCountries: Country[] = [];

  convertedAmount: number | null = null;

  constructor(
    private fb: FormBuilder,
    private quoteService: QuoteService,
    private countryService: CountryService,
    private currencyService: CurrencyService,
    private router: Router
  ) {
    this.quoteForm = this.fb.group({
      mode: [null, Validators.required],
      movementType: [null, Validators.required],
      incoterms: [null, Validators.required],
      packageType: [null, Validators.required],
      fromCurrency: [null, Validators.required],
      toCurrency: [null, Validators.required],
      originalAmount: [null, [Validators.required, Validators.min(0)]],
      country: [null, Validators.required],
      city: [null, Validators.required],
    });
  }

  private selectedCountry: Country | null = null;

  ngOnInit(): void {
    this.quoteForm
      .get('country')
      ?.valueChanges.pipe(
        debounceTime(300),
        distinctUntilChanged(),
        switchMap((value) => {
          if (this.selectedCountry && value === this.selectedCountry.name) {
            return [];
          }
          if (typeof value !== 'string' || value.length < 2) {
            this.selectedCountry = null;
            return [];
          }
          return this.countryService.getCountries(value);
        })
      )
      .subscribe((countries) => (this.filteredCountries = countries));

    this.quoteForm
      .get('originalAmount')
      ?.valueChanges.pipe(debounceTime(400), distinctUntilChanged())
      .subscribe(() => {
        this.onAmountChange();
      });
    this.quoteForm
      .get('fromCurrency')
      ?.valueChanges.pipe(debounceTime(300), distinctUntilChanged())
      .subscribe(() => {
        this.onAmountChange();
      });
  }

  onCountrySelected(country: Country) {
    this.selectedCountry = country;
    this.quoteForm.get('country')?.setValue(country.name, { emitEvent: false });
    this.quoteForm.get('fromCurrency')?.setValue(country.currencyCode);

    if (this.quoteForm.get('originalAmount')?.value) {
      this.onAmountChange();
    }
  }

  onAmountChange(): void {
    const amount = this.quoteForm.get('originalAmount')?.value;
    const fromCurrency = this.quoteForm.get('fromCurrency')?.value;
    const toCurrency = this.quoteForm.get('toCurrency')?.value;

    if (
      amount &&
      fromCurrency &&
      toCurrency &&
      fromCurrency !== 'USD' &&
      toCurrency !== 'USD' &&
      fromCurrency !== toCurrency
    ) {
      const currencyConversion = new CurrencyConversion();
      currencyConversion.fromCurrency = fromCurrency;
      currencyConversion.toCurrency = toCurrency;
      currencyConversion.originalAmount = amount;
      this.currencyService.convertCurrency(currencyConversion).subscribe({
        next: (res) => {
          this.convertedAmount = res.amount;
        },
        error: (err) => {
          console.error('Error converting currency', err);
        },
      });
    } else {
      this.convertedAmount = null;
    }
  }

  onSubmit(): void {
    if (this.quoteForm.invalid) return;

    const quote = this.quoteForm.value;
    quote.convertedAmount = this.convertedAmount;
    this.quoteService.createQuote(quote).subscribe({
      next: () => {
        this.router.navigate(['/quotes/list']);
      },
      error: (err) => {
        console.error('Error creating quote', err);
      },
    });
  }
}
