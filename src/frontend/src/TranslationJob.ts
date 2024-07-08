export interface TranslationJob {
  id: number;
  customerName?: string;
  status?: string;
  originalContent?: string;
  translatedContent?: string;
  price: number;
}

export class TranslationJobImpl implements TranslationJob {
  id: number;
  customerName?: string;
  status?: string;
  originalContent?: string;
  translatedContent?: string;
  price: number;

  constructor(id: number, price: number) {
    this.id = id;
    this.price = price;
  }
}