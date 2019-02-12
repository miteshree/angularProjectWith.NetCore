import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'filter'
})
export class FilterPipe implements PipeTransform {

  transform(names: any, term: any): any {

    //check if search text is undefined
    if (term === undefined) return names;
    //return updated names array

    return names.filter(function (item) {
      return item.name.toLowerCase().includes(term.toLowerCase());
    })

  }

}
