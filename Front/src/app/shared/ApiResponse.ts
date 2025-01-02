export interface ApiResponse<T> {
    result: T;
    message: string;
    isSuccess: boolean;
    errors?: string[],
  }