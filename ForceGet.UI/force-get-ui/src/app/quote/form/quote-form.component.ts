import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { QuoteService } from '../../services/quote.service';
import { CityService } from '../../services/city.service';
import { CurrencyService } from '../../services/currency.service';

import { Mode, MovementType, Incoterms, PackageType, Currency } from '../../models/enums.model';

@Component({
  selector: 'app-quote-form',
  templateUrl: './quote-form.component.html',
  styleUrls: ['./quote-form.component.scss']
})
export class QuoteFormComponent implements OnInit {
  quoteForm: FormGroup;

  // ENUM listeleri
  modes = Object.values(Mode);
  movementTypes = Object.values(MovementType);
  incoterms = Object.values(Incoterms);
  packageTypes = Object.values(PackageType);
  currencies = Object.values(Currency);

  // Dinamik şehir listesi
  cities: string[] = [];

  // USD dönüşümü
  convertedAmount: number | null = null;

  constructor(
    private fb: FormBuilder,
    private quoteService: QuoteService,
    private cityService: CityService,
    private currencyService: CurrencyService,
    private router: Router
  ) {
    this.quoteForm = this.fb.group({
      mode: [null, Validators.required],
      movementType: [null, Validators.required],
      incoterms: [null, Validators.required],
      packageType: [null, Validators.required],
      currency: [null, Validators.required],
      originalAmount: [null, [Validators.required, Validators.min(0)]],
      country: [null, Validators.required],
      city: [null, Validators.required]
    });
  }

  ngOnInit(): void {}

  // Ülke değişince şehirleri getir
  onCountryChange(): void {
    const country = this.quoteForm.get('country')?.value;

    if (!country) return;

    this.cityService.getCitiesByCountry(country).subscribe({
      next: (cities) => {
        this.cities = cities;
      },
      error: (err) => {
        console.error('Error fetching cities', err);
      }
    });
  }

  // Para birimi değişince USD dönüşümü
  onAmountChange(): void {
    const amount = this.quoteForm.get('originalAmount')?.value;
    const currency = this.quoteForm.get('currency')?.value;

    if (amount && currency && currency !== 'USD') {
      this.currencyService.convertToUsd(currency, amount).subscribe({
        next: (res) => {
          this.convertedAmount = res.amount;
        },
        error: (err) => {
          console.error('Error converting currency', err);
        }
      });
    } else {
      this.convertedAmount = null;
    }
  }

  // Form submit
  onSubmit(): void {
    if (this.quoteForm.invalid) return;

    const quote = this.quoteForm.value;

    this.quoteService.createQuote(quote).subscribe({
      next: () => {
        this.router.navigate(['/quotes/list']);
      },
      error: (err) => {
        console.error('Error creating quote', err);
      }
    });
  }
}