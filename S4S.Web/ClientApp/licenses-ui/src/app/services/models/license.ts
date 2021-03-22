export class License {
  name: string;
}

export class LicenseListModel {
  name: string;
  id: number;
  productCount: number;
}

export class LicenseToCreate {
  name: string;
  filePath: string|null;
}
