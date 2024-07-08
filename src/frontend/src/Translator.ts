export interface TranslatorModel {
  id: number;
  name?: string | null;
  horlyRate?: number | null;
  status?: string | null;
  creditCardNumber?: string | null;
}

export class Translator implements TranslatorModel {
  constructor(
    public id: number,
    public name: string | null = null,
    public horlyRate: number | null = null,
    public status: string | null = null,
    public creditCardNumber: string | null = null
  ) {}
}