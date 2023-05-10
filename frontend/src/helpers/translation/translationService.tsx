import { useTranslation } from "react-i18next";

export const useTranslationHelper = () => {
	const { t, i18n } = useTranslation();

	const changeLanguage = (language: string) => {
		i18n.changeLanguage(language);
	};

	return { t, changeLanguage };
};
