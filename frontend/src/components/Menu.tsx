import { useState, useEffect, FC } from "react";
import { Button } from "react-bootstrap";
import { Link } from "react-router-dom";
import { useTranslationHelper } from "../helpers/translation/translationService";

// TODO Languages
export const Menu: FC = () => {
	const { t, changeLanguage } = useTranslationHelper();

	function localizationEN() {
		changeLanguage("en");
		localStorage.clear();
	}
	function localizationUA() {
		changeLanguage("ua");
	}
	function LastElementOfMenu() {
		console.log(localStorage);
		if (localStorage.getItem("userType") !== null) {
			if (localStorage.getItem("userType") === "PrivateCompany") {
				return (
					<Link to="/PrivateCompanyProfile" className="text-decoration-none text-reset">
						<Button className="btn btn-dark border border-white w-100">Profile</Button>
					</Link>
				);
			}
			if (localStorage.getItem("userType") === "LogisticCompany") {
				return (
					<Link to="/LogisticCompanyProfile" className="text-decoration-none text-reset">
						<Button className="btn btn-dark border border-white w-100">Profile</Button>
					</Link>
				);
			}

			if (localStorage.getItem("userType") === "LogisticCompanyAdministrator") {
				return (
					<Link to="/LogisticCompanyAdministratorProfile" className="text-decoration-none text-reset">
						<Button className="btn btn-dark border border-white w-100">Profile</Button>
					</Link>
				);
			}

			if (localStorage.getItem("userType") === "SystemAdmin") {
				return (
					<Link to="/SystemAdminProfile" className="text-decoration-none text-reset">
						<Button className="btn btn-dark border border-white w-100">Admin Panel</Button>
					</Link>
				);
			}
		}
		return (
			<Link to="/Login" className="text-decoration-none text-reset">
				<Button className="btn btn-dark border border-white w-100">Sign in</Button>
			</Link>
		);
	}
	return (
		<div className="Menu">
			<div className="d-inline-flex justify-content-around border w-100 p-3 mb-2 bg-dark text-white">
				<div className="d-flex justify-content-start">
					<div className="mr-2">
						<Link to="/Home" className="text-decoration-none text-reset">
							<Button className="btn btn-dark border border-white w-100">Home</Button>
						</Link>
					</div>
					<div className="mr-2">
						<Link to="/Contacts" className="text-decoration-none text-reset">
							<Button className="btn btn-dark border border-white w-100">Contacts</Button>
						</Link>
					</div>
				</div>
				<div className="d-flex justify-content-end">
					<div className="dropdown m-0 mr-2">
						<button
							className="btn dropdown-toggle btn-dark border border-white"
							type="button"
							data-toggle="dropdown"
							aria-haspopup="true"
							aria-expanded="false">
							{t("Language")}
						</button>
						<div className="dropdown-menu p-0" aria-labelledby="dropdownMenuButton">
							<Button onClick={() => localizationEN()} className="btn btn-dark border border-white w-100">
								EN
							</Button>
							<Button onClick={() => localizationUA()} className="btn btn-dark border border-white w-100">
								UA
							</Button>
						</div>
					</div>
					<div className="mr-2">{LastElementOfMenu()}</div>
				</div>
			</div>
		</div>
	);
};

// export default Menu;
