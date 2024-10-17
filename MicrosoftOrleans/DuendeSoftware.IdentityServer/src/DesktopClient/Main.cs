using IdentityModel.Client;
using System.Text;

namespace DesktopClient
{
	public partial class Main : Form
	{
		private readonly StringBuilder _responseBuffer;

		public Main()
		{
			this._responseBuffer = new StringBuilder();
			InitializeComponent();
		}

		private async void login_Click(object sender, EventArgs e)
		{
			var accessToken = await TokenProvider.RetrieveToken(Common.Config.IdentityServerUrl);
			accessTokenLabel.Text = @"Logged in.";
			accessTokenTextBox.Text = accessToken;
			loadTimeButton.Enabled = true;
		}

		private async void loadTimeButton_Click(object sender, EventArgs e)
		{
			var httpClientHandler = Common.HttpClientExtensions.CreateHttpClientHandler(true);
			var httpClient = new HttpClient(httpClientHandler)
			{
				BaseAddress = new Uri(Common.Config.ApiUrl),
			};
			httpClient.SetBearerToken(accessTokenTextBox.Text);

			var response = await httpClient.GetAsync($"/api/time");
			if (!response.IsSuccessStatusCode)
			{

				MessageBox.Show(response.ReasonPhrase);
			}
			else
			{
				var content = await response.Content.ReadAsStringAsync();
				_responseBuffer.Append(content);
				responseTextBox.Text = _responseBuffer.ToString();
			}
		}
	}
}