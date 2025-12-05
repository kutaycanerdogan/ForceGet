import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'enumLabel',
})
export class EnumLabelPipe implements PipeTransform {
  transform(value: number | string, enumType: any): string {
    const key = typeof value === 'number' ? enumType[value] : value;

    // Eğer tamamen büyük harfse → direkt döndür
    if (/^[A-Z0-9]+$/.test(key)) {
      return key;
    }

    // Normal CamelCase → Label formatı
    return key
      .replace(/([A-Z])/g, ' $1')
      .replace(/\bTo\b/g, 'to')
      .trim();
  }
}
