export interface UpdateModelProps<T> {
	close(): void;
	item: T | undefined;
}
