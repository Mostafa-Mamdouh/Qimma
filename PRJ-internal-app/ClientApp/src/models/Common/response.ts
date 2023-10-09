export interface Response<T> {
  succeeded: boolean;
  message: string;
  logMessage: string;
  errors: string[];
  data: T;
}
