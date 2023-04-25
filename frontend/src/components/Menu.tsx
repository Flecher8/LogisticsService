import { useState, useEffect, FC } from "react";
import { Button } from "react-bootstrap";
import { Link } from "react-router-dom";

// import axios from "../../api/axios";
// import text from "../../services/localizationService";

export const Menu: FC = () => {
	function localizationEN() {}
	function localizationUA() {}
	function LastElementOfMenu() {
		// console.log(localStorage);
		// if (localStorage.getItem("PeopleTracker-userId") !== null) {
		// 	if (localStorage.getItem("PeopleTracker-userType") === "User") {
		// 		return (
		// 			<Link to="/UserProfile" className="text-decoration-none text-reset">
		// 				<Button className="btn btn-dark border border-white w-100">Profile</Button>
		// 			</Link>
		// 		);
		// 	}
		// 	if (localStorage.getItem("PeopleTracker-userType") === "SystemAdmin") {
		// 		return (
		// 			<Link to="/AdminPanelPlacements" className="text-decoration-none text-reset">
		// 				<Button className="btn btn-dark border border-white w-100">Admin Panel</Button>
		// 			</Link>
		// 		);
		// 	}
		// } else {
		// 	return (
		// 		<Link to="/Login" className="text-decoration-none text-reset">
		// 			<Button className="btn btn-dark border border-white w-100">{text("Sign in")}</Button>
		// 		</Link>
		// 	);
		// }
		// return (
		// 	<Link to="/Login" className="text-decoration-none text-reset">
		// 		<Button className="btn btn-dark border border-white w-100">{text("Sign in")}</Button>
		// 	</Link>
		// );
		return (
			<Link to="/Login" className="text-decoration-none text-reset">
				<Button className="btn btn-dark border border-white w-100">Sign in</Button>
			</Link>
		);
	}
	return (
		<div className="Menu">
			<div className="d-inline-flex justify-content-center border w-100 p-3 mb-2 bg-dark text-white">
				<div className="d-flex justify-content-beetween w-50">
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
				<div className="d-flex justify-content-end w-25">
					<div className="dropdown m-0 mr-2">
						<button
							className="btn dropdown-toggle btn-dark border border-white"
							type="button"
							data-toggle="dropdown"
							aria-haspopup="true"
							aria-expanded="false">
							Language
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
