import { useState, useEffect, FC } from "react";
import { Button, InputGroup, FormControl, Table, Modal, Form } from "react-bootstrap";
import { Link } from "react-router-dom";
import { AdminPanel } from "../../components/AdminPanel";
import { LogisticCompaniesService, LogisticCompany } from "../../api/services/LogisticCompaniesService";

// TODO Language
export const AdminLogisticCompanies: FC = () => {
	const [items, setItems] = useState<LogisticCompany[] | null>([]);

	const getItems = async (): Promise<void> => {
		try {
			const response: LogisticCompany[] | null = await LogisticCompaniesService.prototype.getAll();
			setItems(response);
		} catch (err) {
			alert(err);
		}
	};

	useEffect(() => {
		getItems();
	}, []);
	return (
		<div className="AdminLogisticCompanies container">
			{/* // TODO Language */}
			<div className="d-flex border border-dark w-100">
				<AdminPanel />
			</div>
			<div>
				<header>
					<div className="text-center mt-5">
						<h1>Logistic Companies</h1>
					</div>
				</header>

				<main>
					<div className="mt-5">
						<Table className="table table-striped auto__table text-center" striped bordered hover size="lg">
							<thead>
								<tr>
									<th>Id</th>
									<th>Company name</th>
									<th>email</th>
								</tr>
							</thead>
							<tbody>
								{items ? (
									items.map(e => (
										<tr key={e.logisticCompanyId}>
											<td>{e.logisticCompanyId}</td>
											<td>{e.companyName}</td>
											<td>{e.email}</td>
										</tr>
									))
								) : (
									<tr>
										<td colSpan={5}>No data</td>
									</tr>
								)}
							</tbody>
						</Table>
					</div>
				</main>
			</div>
		</div>
	);
};
