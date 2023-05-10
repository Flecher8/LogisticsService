import { useState, useEffect, FC } from "react";
import { Button } from "react-bootstrap";
import { Link } from "react-router-dom";
import { useTranslationHelper } from "../helpers/translation/translationService";

export interface ProfilePanelElement {
	name: string;
	linkTo: string;
}

export interface ProfilePanelProps {
	profilePanelElements: ProfilePanelElement[];
}

export const ProfilePanel: FC<ProfilePanelProps> = ({ profilePanelElements }) => {
	const { t, changeLanguage } = useTranslationHelper();

	function clearUserInfo() {
		localStorage.clear();
	}

	const signOut = async (e: React.MouseEvent<HTMLButtonElement, MouseEvent>) => {
		e.preventDefault();
		clearUserInfo();
		window.location.href = "/Home";
	};

	return (
		<div className="ProfilePanel">
			<div className="d-inline-flex w-100 p-3">
				<div className="d-inline-flex justify-content-start flex-wrap w-100">
					{profilePanelElements.map((element, i) => (
						<div className="" key={i}>
							<Link to={element.linkTo} className="text-decoration-none text-reset">
								<Button className="btn btn-dark border border-white">{element.name}</Button>
							</Link>
						</div>
					))}
					<div className="">
						<Link to="/" className="text-decoration-none text-reset">
							<Button onClick={signOut} className="btn btn-dark border border-white">
								{t("Sign Out")}
							</Button>
						</Link>
					</div>
				</div>
			</div>
		</div>
	);
};
