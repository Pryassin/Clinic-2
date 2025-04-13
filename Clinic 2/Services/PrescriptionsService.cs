using DataLayer.Data;
using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;

public class PrescriptionsService :  IPrescriptions
{
    private readonly IPrescriptionsRepository _prescriptionsRepository;
    public PrescriptionsService(IPrescriptionsRepository prescriptionsRepository)
    {
        _prescriptionsRepository = prescriptionsRepository;
    }

}

