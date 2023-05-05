import { useState, useEffect, FC } from "react";
import { Button, Card } from "react-bootstrap";
import { Form, Link } from "react-router-dom";
import { AdminPanel } from "../../components/AdminPanel";
import { PrivateCompanyPanel } from "../../components/PrivateCompanyPanel";
import { LogisticCompanyRates, RatesService } from "../../api/services/RatesService";
import { RatesList } from "../../components/RatesList";

export const PrivateCompanyCreateOrder: FC = () => {
	const [logisticCompanyRates, setLogisticCompanyRates] = useState<LogisticCompanyRates[] | null>(null);
	const [selectedItemId, setSelectedItemId] = useState<number>(0);
	const getLogisticCompanyRates = async (): Promise<void> => {
		try {
			const response: LogisticCompanyRates[] | null = await RatesService.prototype.getRates();
			if (response === null) return;
			setLogisticCompanyRates(response);
		} catch (err) {}
	};

	useEffect(() => {
		getLogisticCompanyRates();
	}, []);
	return (
		<div className="PrivateCompanyCreateOrder container">
			{/* // TODO Language */}
			<div className="d-flex border border-dark w-100">
				<PrivateCompanyPanel />
			</div>
			<div>
				<header>
					<div className="text-center mt-5">
						<h1>Create order</h1>
					</div>
				</header>
				<div>
					<div>
						<h3>Select logistic company</h3>
					</div>
					<div>
						{logisticCompanyRates !== null ? (
							<RatesList
								logisticCompanyRates={logisticCompanyRates === null ? [] : logisticCompanyRates}
								selectedItemId={selectedItemId}
								onSelectItem={setSelectedItemId}
							/>
						) : (
							<p>No data</p>
						)}
					</div>
				</div>
				{/* <Form></Form> */}
			</div>
		</div>
	);
};
