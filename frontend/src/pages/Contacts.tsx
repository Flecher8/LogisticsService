import { useState, useEffect, FC } from "react";
import { Button } from "react-bootstrap";
import { Link } from "react-router-dom";
import { useTranslationHelper } from "../helpers/translation/translationService";

export const Contacts: FC = () => {
	const { t, changeLanguage } = useTranslationHelper();
	return (
		<div className="Contacts">
			<h1>{t("System made by Vladyslav Bocharov")}</h1>
		</div>
	);
};
