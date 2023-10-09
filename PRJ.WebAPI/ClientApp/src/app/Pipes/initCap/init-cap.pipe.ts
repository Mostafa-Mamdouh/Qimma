import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'initCap',
})
export class InitCapPipe implements PipeTransform {
  transform(word: string, ...args: unknown[]): unknown {
    if (!word) return word;
    return word[0].toUpperCase() + word.substr(1).toLowerCase();
  }
}
