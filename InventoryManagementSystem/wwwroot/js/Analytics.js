var popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'))
var popoverList = popoverTriggerList.map(function (popoverTriggerEl) {
	return new bootstrap.Popover(popoverTriggerEl)
})

let revenueByMonth = document.getElementById("revenue-by-month").getContext('2d')
let ordersByMonth = document.getElementById("orders-by-month").getContext('2d')
let revenueByCategory = document.getElementById("revenue-by-category").getContext('2d')

let revenueByMonthChart = new Chart(revenueByMonth, {
	type: 'bar',
	data: {
		labels: MonthList,
		datasets: [

			{
				label: 'Total Revenue',
				data: MonthRevenueList,
				backgroundColor: "#3b82ec",
				borderRadius: 100,
				barPercentage: .5
			}
		]
	},
	options: {
		scales: {
			y: {
				stacked: true,
				title: {
					display: true,
					text: "Revenue ($)",
				},
				grid: {
					display: false
				}
			},

			x: {
				stacked: true,
				title: {
					display: true,
					text: "Month",
				},
				grid: {
					display: false
				}
			}
		},
		plugins: {
			legend: {
				labels: {
					usePointStyle: true
				},
			}
		}
	}
});

let ordersByMonthChart = new Chart(ordersByMonth, {
	type: 'bar',
	data: {
		labels: MonthList,
		datasets: [
			{
				label: "Orders Placed",
				backgroundColor: "#3b82ec",

				borderWidth: 1,
				data: MonthTotalOrdersPlaced,
				borderRadius: 100
			},
			{
				label: "Orders Completed (On Time)",
				backgroundColor: "#66c2a5",

				borderWidth: 1,
				data: MonthOnTimeOrderList,
				stack: "stack 0",
				borderRadius: 100
			},
			{
				label: "Orders Completed (Past Due)",
				backgroundColor: "#f46d43",

				borderWidth: 1,
				data: MonthPastDueOrderList,
				stack: "stack 0",
				borderRadius: 100
			}
		]
	},
	options: {
		scales: {
			y: {
				title: {
					display: true,
					text: "# Orders",
				},
				grid: {
					display: false
				}
			},

			x: {
				title: {
					display: true,
					text: "Month",
				},
				grid: {
					display: false
				}
			}
		},
		plugins: {
			legend: {
				labels: {
					usePointStyle: true,
					padding: 20
				},
			}
		}
	}
});

let revenueByCategoryChart = new Chart(revenueByCategory, {
	type: 'doughnut',
	data: {
		labels: categoryNamesList,
		datasets: [
			{
				label: 'Orders by Category',
				data: categoryOrderCount,
				backgroundColor: [

					"#3288bd",
					"#66c2a5",
					"#abdda4",
					"#e6f598",
					"#fee08b",
					"#fdae61",
					"#f46d43",
					"#d53e4f",

				],
				barPercentage: .5
			}

		]
	},
	options: {
		plugins: {
			legend: {
				display: false
			}
		},
		cutout: "75%",
		aspectRatio: 2
	}
});